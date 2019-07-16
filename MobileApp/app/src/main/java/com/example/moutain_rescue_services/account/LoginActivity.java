package com.example.moutain_rescue_services.account;

import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.moutain_rescue_services.services.AuthanticationService;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.concurrent.ExecutionException;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import javax.net.ssl.HttpsURLConnection;

public class LoginActivity extends AppCompatActivity {

    private static final String TAG = "PhoneFragment";
    private static final String fileName = "UserInfo";

    private EditText phoneNumber;

    Context context;

    AuthanticationService authService;

    public  LoginActivity(){
        context = this;
        this.authService = new AuthanticationService(context);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        if(authService.IsAuthanticated()){
            Intent intent = new Intent(context, GpsActivity.class);
            startActivity(intent);
            finish();
        }

        phoneNumber = (EditText) findViewById(R.id.phonenumber);
        Button sendSms = (Button) findViewById(R.id.register);

        sendSms.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast.makeText(context, "Going to register fragment",Toast.LENGTH_SHORT).show();

                String phoneNum = phoneNumber.getText().toString().trim();

                // SaveFile(phoneNum);

                boolean IsRegistered = authService.RegisterUser(phoneNum);

                if(!IsRegistered){
                    //Display an error
                    return;
                }

                Intent intent = new Intent(context, VerificationActivity.class);
                startActivity(intent);
            }
        });
    }
}
