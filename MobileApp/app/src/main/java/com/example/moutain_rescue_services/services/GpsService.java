package com.example.moutain_rescue_services.services;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;
import android.widget.Toast;

import com.example.moutain_rescue_services.common.GlobalConstants;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.concurrent.ExecutionException;

public class GpsService {

    private Context context;

    private String authToken;

    private AuthenticationService authService;

    private FileService fileService;

    public GpsService(Context context){
        this.context = context;
        this.authService = new AuthenticationService(context);
        this.fileService = new FileService(context);
    }

    public boolean SendLocation(double latitude, double longitude, double altitude){
        SendLocation sendLocation = new SendLocation();

        authToken = this.fileService.ReadUserInfo(GlobalConstants.UserFile).trim();

        try {
            int statusCode = sendLocation.execute(latitude, longitude, altitude).get();

            if(statusCode == 201){
                return true;
            }

        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        return false;
    }

    public boolean SendLocationWithMessage(double latitude, double longitude, double altitude, String message, String condition){

        SendLocationWithMessageAndCondition sendLocationWithMessageAndCondition = new SendLocationWithMessageAndCondition();

        authToken = this.fileService.ReadUserInfo(GlobalConstants.UserFile).trim();

        try {
            //Adding "" to cast them to string
            int statusCode = sendLocationWithMessageAndCondition.execute((latitude)+"", longitude+"", altitude+"", message, condition).get();

            if(statusCode == 201){
                return true;
            }

        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }


        return  false;
    }

    private class SendLocation extends AsyncTask<Double,Integer,Integer> {
        @Override
        protected Integer doInBackground(Double... doubles) {
            try{
                return sendLocation(doubles[0], doubles[1], doubles[2]);
            }catch (IOException e ){
                Toast.makeText(context, "Something went wrong", Toast.LENGTH_SHORT).show();
                return 0;
            }
            catch (JSONException ex) {
                return 0;
            }
        }
    }

    private Integer sendLocation(double latitude, double longitude, double altitude) throws  IOException,JSONException{
        InputStream is = null;

        int statusCode = 0;

        try {
            URL url = new URL(GlobalConstants.URL + "/api/location/addlocation");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Authorization", "Bearer " + authToken);
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);

            JSONObject jsonParam = new JSONObject();
            jsonParam.put("Longitude", longitude);
            jsonParam.put("Latitude", latitude);
            jsonParam.put("Altitude", altitude);

            Log.i("JSON", jsonParam.toString());
            DataOutputStream os = new DataOutputStream(conn.getOutputStream());
            //os.writeBytes(URLEncoder.encode(jsonParam.toString(), "UTF-8"));
            os.writeBytes(jsonParam.toString());

            os.flush();
            os.close();

            Log.i("STATUS", String.valueOf(conn.getResponseCode()));
            Log.i("MSG", conn.getResponseMessage());

            is = conn.getInputStream();

            statusCode = conn.getResponseCode();

            conn.disconnect();

            return statusCode;

        }
        finally {
            if (is != null){
                is.close();
            }
        }
    }

    private class SendLocationWithMessageAndCondition extends AsyncTask<String,Integer,Integer> {
        @Override
        protected Integer doInBackground(String... args) {
            try{
                return sendLocationWithMessageAndCondition(args[0], args[1], args[2], args[3], args[4]);
            }catch (IOException e ){
                Toast.makeText(context, "Something went wrong", Toast.LENGTH_SHORT).show();
                return 0;
            }
            catch (JSONException ex) {
                return 0;
            }
        }
    }

    private Integer sendLocationWithMessageAndCondition(String latitude, String longitude, String altitude,String message, String condition) throws  IOException,JSONException{
        InputStream is = null;

        int statusCode = 0;

        try {
            URL url = new URL(GlobalConstants.URL + "/api/location/addlocationwithmessage");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Authorization", "Bearer " + authToken);
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);

            JSONObject jsonParam = new JSONObject();
            JSONObject jsonInnerObject = new JSONObject();
            jsonParam.put("Longitude", Double.parseDouble(longitude));
            jsonParam.put("Latitude", Double.parseDouble(latitude));
            jsonParam.put("Altitude", Double.parseDouble(altitude));
            jsonInnerObject.put("Message", message);
            jsonInnerObject.put("Condition", condition);
            jsonParam.put("Message", jsonInnerObject);

            Log.i("JSON", jsonParam.toString());
            DataOutputStream os = new DataOutputStream(conn.getOutputStream());
            //os.writeBytes(URLEncoder.encode(jsonParam.toString(), "UTF-8"));
            os.writeBytes(jsonParam.toString());

            os.flush();
            os.close();

            Log.i("STATUS", String.valueOf(conn.getResponseCode()));
            Log.i("MSG", conn.getResponseMessage());

            is = conn.getInputStream();

            statusCode = conn.getResponseCode();

            conn.disconnect();

            return statusCode;

        }
        finally {
            if (is != null){
                is.close();
            }
        }
    }

}
