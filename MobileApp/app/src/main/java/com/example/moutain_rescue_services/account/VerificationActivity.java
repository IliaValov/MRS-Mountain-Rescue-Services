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

    private EditText verificationCode;

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

        verificationCode = findViewById(R.id.verificationCode);
        Button register = findViewById(R.id.checkVerification);
        TextView textView = findViewById(R.id.textView3);

        register.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                String verifyCode = verificationCode.getText().toString();

                boolean isUserVerify = authService.VerifyUser(verifyCode);

                if(!isUserVerify){
                    Toast.makeText(context, "Invalid verification code", Toast.LENGTH_SHORT).show();
                    return;
                }

                Intent intent = new Intent(context, GpsActivity.class);
                startActivity(intent);
                finish();
            }
        });
    }
}