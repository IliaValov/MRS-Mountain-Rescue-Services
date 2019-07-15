package com.example.moutain_rescue_services.services;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.concurrent.ExecutionException;

import javax.net.ssl.HttpsURLConnection;

public class AuthanticationService {

    private Context context;
    private String fileName = "UserInfo";
    private FileService fileService;

    public AuthanticationService(Context context) {
        this.context = context;
        this.fileService = new FileService(context);
    }

    public boolean RegisterUser(String phone) {

        RegisterUser registerUser = new RegisterUser();

        try {
            int result = registerUser.execute(phone).get();

            if (result == 201) {
                return true;
            }

        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        return false;
    }

    public boolean VerifyUser() {
        String[] userInfo = fileService.ReadUserInfo(fileName).split("\n");

        if(userInfo.length < 2){
            return false;
        }

        String phoneNumber = userInfo[0];
        String secretKey = userInfo[1];

        SendUserVerification userVerification = new SendUserVerification();

        try {
            int statusCode = userVerification.execute().get();

            if(statusCode == 200){
                return  true;
            }

        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        return false;
    }

    public boolean IsAuthanticated() {
        String[] userInfo = fileService.ReadUserInfo(fileName).split("\n");

        String userPhoneNumber = userInfo[0];
        String userBearer = userInfo[1];

        RequestTask requestTask = new RequestTask();

        try {
            int result = requestTask.execute(userPhoneNumber, userBearer).get();

            if (result == 200) {
                return false;
            }

        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }


        return false;
    }


    private static class RequestTask extends AsyncTask<String, Integer, Integer> {

        @Override
        protected Integer doInBackground(String... args) {
            int result = 0;
            String bearer = args[0];
            try {
                URL reqURL = new URL("http://public-localization-services-authentication.azurewebsites.net/mobilelogin/"); //the URL we will send the request to
                HttpURLConnection request = (HttpURLConnection) (reqURL.openConnection());
                request.setRequestProperty("Authorization","Bearer " + bearer);
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
        int statusCode;

        try {
            URL url = new URL("http://public-localization-services-authentication.azurewebsites.net/add/mobileregister");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);

            JSONObject jsonParam = new JSONObject();
            jsonParam.put("PhoneNumber", phoneNumber);

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

            StringBuilder response = new StringBuilder();

            if (responseCode == HttpsURLConnection.HTTP_OK) {
                String line;
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                while ((line = br.readLine()) != null) {
                    response.append(line);
                }

            } else {
                response = new StringBuilder("empty");
            }

            if (!response.toString().equals("empty")) {
                fileService.SaveUserRegisterInfoFile(phoneNumber, response.toString());
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

    private Integer sendUserVerification(String phoneNumber, String secretKey, String verificationCode) throws IOException, JSONException {
        InputStream is = null;
        int statusCode = 0;
        String authToken;

        try {
            URL url = new URL("http://public-localization-services-authentication.azurewebsites.net/mobilelogin");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);

            JSONObject jsonParam = new JSONObject();
            jsonParam.put("PhoneNumber", phoneNumber);
            jsonParam.put("VerificationCode", verificationCode);
            jsonParam.put("SecretKey", secretKey);

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

            String response = "";

            if (responseCode == HttpsURLConnection.HTTP_OK) {
                authToken = conn.getHeaderField("access-token");
            } else {
                authToken = "empty";
            }

            if(!authToken.equals("empty")){
                fileService.SaveToken(authToken);
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


