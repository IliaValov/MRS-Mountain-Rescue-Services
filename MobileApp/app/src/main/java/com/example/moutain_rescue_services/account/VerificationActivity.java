package com.example.moutain_rescue_services.account;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.moutain_rescue_services.services.AuthanticationService;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import javax.net.ssl.HttpsURLConnection;


public class VerificationActivity  extends Activity {

    private String phoneNumber = "";
    private String secretKey = "";

    private EditText verificationCode;
    private static final String fileName = "UserInfo";

    Context context;
    AuthanticationService authService;

    public VerificationActivity(){
        this.context = this;
        this.authService = new AuthanticationService(context);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        context = this;

        setContentView(R.layout.activity_verify_phone_number);

        verificationCode = (EditText) findViewById(R.id.verificationCode);
        Button register = (Button) findViewById(R.id.checkVerification);
        TextView textView = findViewById(R.id.textView3);

        register.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast.makeText(context, "Go to GpsActivity", Toast.LENGTH_SHORT).show();


                String verifyCode = verificationCode.getText().toString();

                boolean isUserVerify = authService.VerifyUser();

                if(!isUserVerify){
                    //TODO Display an error
                }

                Intent intent = new Intent(context, GpsActivity.class);
                startActivity(intent);
                finish();
            }
        });
    }

}