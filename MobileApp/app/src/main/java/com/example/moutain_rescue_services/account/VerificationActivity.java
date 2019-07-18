package com.example.moutain_rescue_services.account;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.moutain_rescue_services.services.AuthenticationService;


public class VerificationActivity  extends Activity {

    private String phoneNumber = "";
    private String secretKey = "";

    private EditText verificationCode;
    private static final String fileName = "UserInfo";

    Context context;
    AuthenticationService authService;

    public VerificationActivity(){
        this.context = this;
        this.authService = new AuthenticationService(context);
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