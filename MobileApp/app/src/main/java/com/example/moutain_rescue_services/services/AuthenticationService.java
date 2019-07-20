package com.example.moutain_rescue_services.services;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;
import android.widget.Toast;

import com.example.moutain_rescue_services.common.GlobalConstants;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLEncoder;
import java.nio.charset.StandardCharsets;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ExecutionException;

import javax.net.ssl.HttpsURLConnection;

public class AuthenticationService {

    private FileService fileService;
    Context context;

    public AuthenticationService(Context context) {

        this.context = context;

        this.fileService = new FileService(context);
    }

    public boolean RegisterUser(String phone) {

        if(!internetIsConnected()){

            Toast.makeText(context,
                    "No internet check your internet provider", Toast.LENGTH_SHORT).show();
            return false;
        }

        RegisterUser registerUser = new RegisterUser();

        try {
            int result = registerUser.execute(phone).get();

            if (result == 200) {
                return true;
            }

        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        return false;
    }

    public boolean VerifyUser(String verificationCode) {
        if(!internetIsConnected()){

            Toast.makeText(context,
                    "No internet check your internet provider", Toast.LENGTH_SHORT).show();
            return false;
        }

        String[] userInfo = fileService.ReadUserInfo(GlobalConstants.UserFile).split("\n");

        if (userInfo.length < 2) {
            return false;
        }

        String phoneNumber = userInfo[0];
        String secretKey = userInfo[1];

        SendUserVerification userVerification = new SendUserVerification();

        try {
            int statusCode = userVerification.execute(phoneNumber, secretKey, verificationCode).get();

            if (statusCode == 200) {
                return true;
            }

        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        return false;
    }

    private boolean internetIsConnected() {
        try {
            String command = "ping -c 1 google.com";
            return (Runtime.getRuntime().exec(command).waitFor() == 0);
        } catch (Exception e) {
            return false;
        }
    }

    public boolean IsAuthenticated() {

        if(!internetIsConnected()){

            Toast.makeText(context,
                    "No internet check your internet provider", Toast.LENGTH_SHORT).show();
            return false;
        }

        String[] userInfo = fileService.ReadUserInfo(GlobalConstants.UserFile).split("\n");

        if (userInfo.length < 1) {
            return false;
        }

        String userBearer = userInfo[0];

        Authanticate requestTask = new Authanticate();

        try {
            int result = requestTask.execute(userBearer).get();

            if (result == 200) {
                return true;
            }

        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }


        return false;
    }


    private static class Authanticate extends AsyncTask<String, Integer, Integer> {

        @Override
        protected Integer doInBackground(String... args) {
            int result = 0;
            String bearer = args[0];
            try {
                URL reqURL = new URL(GlobalConstants.URL + "/api/account/authanticate"); //the URL we will send the request to
                HttpURLConnection request = (HttpURLConnection) (reqURL.openConnection());
                request.setRequestProperty("Authorization", "Bearer " + bearer);
                request.setRequestMethod("GET");
                request.connect();

                result = request.getResponseCode();
            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }


            return result;
        }
    }

    private class RegisterUser extends AsyncTask<String, Integer, Integer> {
        @Override
        protected Integer doInBackground(String... strings) {
            try {
                return registerUser(strings[0]);
            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } catch (JSONException e) {
                e.printStackTrace();
            }

            return 0;
        }
    }

    private Integer registerUser(String phoneNumber) throws IOException, JSONException {
        InputStream is = null;
        String response = "";

        try {
            URL url = new URL(GlobalConstants.URL + "/api/account/register");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);

            JSONObject jsonParam = new JSONObject();
            jsonParam.put("PhoneNumber", phoneNumber);
            jsonParam.put("Device", "Lenovo");

            Log.i("JSON", jsonParam.toString());
            DataOutputStream os = new DataOutputStream(conn.getOutputStream());
            //os.writeBytes(URLEncoder.encode(jsonParam.toString(), "UTF-8"));
            os.writeBytes(jsonParam.toString());

            os.flush();
            os.close();

            Log.i("STATUS", String.valueOf(conn.getResponseCode()));
            Log.i("MSG", conn.getResponseMessage());

            is = conn.getInputStream();

            int responseCode = conn.getResponseCode();

            if (responseCode == HttpsURLConnection.HTTP_OK) {
                String line;
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                while ((line = br.readLine()) != null) {
                    response += line;
                }

            } else {
                response = "empty";
            }

            if (!response.equals("empty")) {
                fileService.SaveUserRegisterInfoFile(phoneNumber, response);
            }

            conn.disconnect();

            return responseCode;

        } finally {
            if (is != null) {
                is.close();
            }
        }
    }

    private class SendUserVerification extends AsyncTask<String, Integer, Integer> {
        @Override
        protected Integer doInBackground(String... strings) {
            String phoneNumber = strings[0];
            String secretKey = strings[1];
            String verificationCode = strings[2];

            try {
                return sendUserVerification(phoneNumber, secretKey, verificationCode);
            } catch (IOException e) {

                return 0;
            } catch (JSONException ex) {
                return 0;
            }
        }
    }

    private String getDataString(HashMap<String, String> params) throws UnsupportedEncodingException {
        StringBuilder result = new StringBuilder();
        boolean first = true;
        for (Map.Entry<String, String> entry : params.entrySet()) {
            if (first)
                first = false;
            else
                result.append("&");
            result.append(URLEncoder.encode(entry.getKey(), "UTF-8"));
            result.append("=");
            result.append(URLEncoder.encode(entry.getValue(), "UTF-8"));
        }
        return result.toString();
    }

    private Integer sendUserVerification(String phoneNumber, String secretKey, String verificationCode) throws IOException, JSONException {
        InputStream is = null;
        int statusCode = 0;
        String authToken;

        HashMap<String, String> hashMap = new HashMap<String, String>();

        hashMap.put("PhoneNumber", phoneNumber);
        hashMap.put("VerificationCode", verificationCode);
        hashMap.put("SecretKey", secretKey);

        String encodedData = getDataString(hashMap);

        byte[] postData = encodedData.getBytes();

        try {
            URL url = new URL(GlobalConstants.URL + "/api/account/login");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setDoInput(true);
            conn.setDoOutput(true);
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");

            String data = URLEncoder.encode("phonenumber", "UTF-8")
                    + "=" + URLEncoder.encode(phoneNumber, "UTF-8");

            data += "&" + URLEncoder.encode("verificationcode", "UTF-8") + "="
                    + URLEncoder.encode(verificationCode, "UTF-8");

            data += "&" + URLEncoder.encode("token", "UTF-8") + "="
                    + URLEncoder.encode(secretKey, "UTF-8");

            DataOutputStream os = new DataOutputStream(conn.getOutputStream());
            //os.writeBytes(URLEncoder.encode(encodedData, "UTF-8"));
            os.writeBytes(data);

            os.flush();
            os.close();

            Log.i("STATUS", String.valueOf(conn.getResponseCode()));
            Log.i("MSG", conn.getResponseMessage());

            is = conn.getInputStream();

            int responseCode = conn.getResponseCode();

            String response = "";

            if (responseCode == HttpsURLConnection.HTTP_OK) {
                String line;
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                while ((line = br.readLine()) != null) {
                    response += line;
                }

            } else {
                response = "empty";
            }


            if (!response.equals("empty")) {
                try {
                    JSONObject jsonObject = new JSONObject(response);
                    String jwt = jsonObject.getString("access_token");
                    fileService.SaveToken(jwt);
                    Log.i("MSG", jwt);
                } catch (JSONException err) {
                    Log.d("Error", err.toString());
                }


            }

            statusCode = conn.getResponseCode();

            conn.disconnect();

            return statusCode;

        } finally {
            if (is != null) {
                is.close();
            }
        }
    }
}


