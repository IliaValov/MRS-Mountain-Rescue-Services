package com.example.moutain_rescue_services.account;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.PopupWindow;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import android.widget.Toast;

import com.example.moutain_rescue_services.services.GpsService;

public class GpsActivity extends Activity implements SensorEventListener {

    private Context context;

    GpsService gpsService;

    LocationManager locationManager;

    private SensorEventListener sensorEventListener;

    private SensorManager senSensorManager;
    private Sensor senAccelerometer;

    RadioGroup radioGroup;
    RadioButton radioCondition;

    EditText conditionMessage;

    private Button saveMeButton;

    private Double currentLatitude;
    private Double currentLongitude;
    private Double currentAltitude;

    public GpsActivity() {
        this.context = this;
        gpsService = new GpsService(context);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        sensorEventListener = this;

        setContentView(R.layout.activity_gps);

        setContentView(R.layout.activity_gps);

        saveMeButton = findViewById(R.id.saveMeButton);

        locationManager = (LocationManager) context.getSystemService(LOCATION_SERVICE);

        // Define a listener that responds to location updates
        LocationListener locationListener = new LocationListener() {
            public void onLocationChanged(Location loc) {
                // Called when a new location is found by the network location provider.
                String locStr = String.format("%s %f:%f (%f meters)", loc.getProvider(),
                        loc.getLatitude(), loc.getLongitude(), loc.getAccuracy());
                TextView tvLoc = (TextView) findViewById(R.id.position1);
                tvLoc.setText(locStr);

                currentLatitude = loc.getLatitude();
                currentLongitude = loc.getLongitude();
                currentAltitude = loc.getAltitude();

                gpsService.SendLocation(currentLatitude, currentLongitude, currentAltitude);

                Log.v("Gibbons", locStr);
            }

            public void onStatusChanged(String provider, int status, Bundle extras) {
                Log.v("Gibbons", "location onStatusChanged() called");
            }

            public void onProviderEnabled(String provider) {
                Log.v("Gibbons", "location onProviderEnabled() called");
            }

            public void onProviderDisabled(String provider) {
                Log.v("Gibbons", "location onProviderDisabled() called");
            }
        };

        // Register the listener with the Location Manager to receive location updates
        Log.v("Gibbons", "setting location updates from network provider");
        if (ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.
            return;
        }
        locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 60000, 20, locationListener);
        Log.v("Gibbons", "setting location updates from GPS provider");
        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 60000, 20, locationListener);


        senSensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        senAccelerometer = senSensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        senSensorManager.registerListener(sensorEventListener, senAccelerometer, SensorManager.SENSOR_DELAY_NORMAL);

        saveMeButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
//                        Intent intent = new Intent(GpsActivity.this, PopEmergencyWindow.class);
//                        startActivity(intent);
                onButtonShowPopupWindowClick(v);
            }
        });
    }

    @Override
    public void onSensorChanged(SensorEvent sensorEvent) {
        Sensor mySensor = sensorEvent.sensor;

        if (mySensor.getType() == Sensor.TYPE_ACCELEROMETER) {
            float x = sensorEvent.values[0];
            float y = sensorEvent.values[1];
            float z = sensorEvent.values[2];
            TextView tvLoc = findViewById(R.id.position2);
            String xyzStr = String.format("Position: %f:%f %f ", x, y, z);
            tvLoc.setText(xyzStr);
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }

    public void onButtonShowPopupWindowClick(View view) {

        // inflate the layout of the popup window
        LayoutInflater inflater = (LayoutInflater)
                getSystemService(LAYOUT_INFLATER_SERVICE);
        final View popupView = inflater.inflate(R.layout.activity_emergency, null);

        radioGroup = popupView.findViewById(R.id.RadioConditionGroup);

        conditionMessage = popupView.findViewById(R.id.message);

        Button proceed = popupView.findViewById(R.id.proceed);
        Button goBack = popupView.findViewById(R.id.goBack);

        // create the popup window
        int width = LinearLayout.LayoutParams.WRAP_CONTENT;
        int height = LinearLayout.LayoutParams.WRAP_CONTENT;
        boolean focusable = true; // lets taps outside the popup also dismiss it
        final PopupWindow popupWindow = new PopupWindow(popupView, width, height, focusable);

        // show the popup window
        // which view you pass in doesn't matter, it is only used for the window tolken
        popupWindow.showAtLocation(view, Gravity.CENTER, 0, 0);

        proceed.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {


                // get selected radio button from radioGroup
                int selectedId = radioGroup.getCheckedRadioButtonId();

                if (selectedId == -1) {
                    Toast.makeText(context,
                            "Select condition", Toast.LENGTH_SHORT).show();
                    return;
                }

                // find the radiobutton by returned id
                radioCondition = (RadioButton) popupView.findViewById(selectedId);


                boolean isSend = gpsService
                        .SendLocationWithMessage(currentLatitude, currentLongitude, currentAltitude,
                                conditionMessage.getText().toString(), radioCondition.getText().toString());
                Toast.makeText(context,
                        "Your situation is send", Toast.LENGTH_SHORT).show();

                popupWindow.dismiss();
                return;
            }

        });

        goBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                popupWindow.dismiss();
                return;
            }
        });

        // dismiss the popup window when touched
        popupView.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                popupWindow.dismiss();
                return true;
            }
        });
    }
}