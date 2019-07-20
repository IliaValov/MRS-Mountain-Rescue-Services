package com.example.moutain_rescue_services.account;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.net.ConnectivityManager;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.moutain_rescue_services.services.AuthenticationService;

public class LoginActivity extends AppCompatActivity {

    Context context;

    ConnectivityManager connectivityManager;

    AuthenticationService authService;

    private EditText phoneNumber;

    public  LoginActivity(){
        context = this;
        this.authService = new AuthenticationService(context);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        ActivityCompat.requestPermissions(this,new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, 1);

        if(authService.IsAuthenticated()){
            Intent intent = new Intent(context, GpsActivity.class);
            startActivity(intent);
            finish();
        }

        phoneNumber = (EditText) findViewById(R.id.phonenumber);

        Button sendSms = (Button) findViewById(R.id.register);

        sendSms.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String phoneNum = phoneNumber.getText().toString().trim();

                // SaveFile(phoneNum);

                boolean IsRegistered = authService.RegisterUser(phoneNum);

                if(!IsRegistered){
                    //Display an error

                    Toast.makeText(context, "Invalid phone number",Toast.LENGTH_SHORT).show();
                    return;
                }

                Intent intent = new Intent(context, VerificationActivity.class);
                startActivity(intent);
            }
        });


        if (ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.
            ActivityCompat.requestPermissions(this,new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, 1);
            return;
        }

    }
}
