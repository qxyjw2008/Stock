package com.stockside;

import android.app.Activity;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.view.Menu;
import android.view.MenuItem;


import java.util.Date;

import android.support.v7.app.ActionBarActivity;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SimpleCursorAdapter.ViewBinder;
import android.R.integer;
import android.R.string;
import android.database.DatabaseUtils;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.os.Build;
import android.view.Window;
import android.content.*;
import android.widget.*;
import android.view.*;

public class LoginActivity extends ActionBarActivity implements View.OnClickListener
{
	private ActionBar actionBar;
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		
		
		getSupportActionBar().setDisplayOptions(ActionBar.DISPLAY_SHOW_CUSTOM);
		setContentView(R.layout.activity_login);
		getSupportActionBar().setCustomView(R.layout.title);
		
		//TextView view = (TextView) findViewById(R.id.textView1);
		//view.setGravity(Gravity.CENTER);
		
		initView();
		
		initData();
		
		Button btn1 =(Button)findViewById(R.id.btn_login);
		btn1.setOnClickListener(new View.OnClickListener(){    
            @Override  
            public void onClick(View v) {  
                Intent intent = new Intent();  
                intent.setClass(LoginActivity.this, StockSideActivity.class);  
                startActivity(intent);  
                finish();//ֹͣ��ǰ��Activity,�����д,�򰴷��ؼ�����ת��ԭ����Activity        
            }  
              
        }); 
	}
	
	@Override
	public void onClick(View v) 
	{
		int ItemId = v.getId();//��ȡ�����idֵ
		switch (ItemId) 
		{
		
		}
	}
	
	private void initView()
	{
		//�õ�ActionBar
		actionBar = getSupportActionBar();
	}
    
    private void initData()
	{
    	//actionBar.setTitle("StockSide");
    	actionBar.setDisplayShowHomeEnabled(false);
    	actionBar.setDisplayHomeAsUpEnabled(false);
		//����ActionBar���ⲻ��ʾ
		actionBar.setDisplayShowTitleEnabled(true);
		
		//����ActionBar�ı���
		//actionBar.setBackgroundDrawable(getResources().getDrawable(R.drawable.actionbar_gradient_bg));
		
		//����ActionBar���Ĭ�ϵ�ͼ���Ƿ����
		actionBar.setDisplayUseLogoEnabled(false);
		
		//���õ���ģʽΪTabѡ���ǩ����ģʽ
		//actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_TABS);
		
		//����ActinBar����Tabѡ���ǩ
		//actionBar.addTab(actionBar.newTab().setText("TAB1").setTabListener(new MyTabListener<FragmentPage1>(this,FragmentPage1.class)));
		//actionBar.addTab(actionBar.newTab().setText("TAB2").setTabListener(new MyTabListener<FragmentPage2>(this,FragmentPage2.class)));
		//actionBar.addTab(actionBar.newTab().setText("TAB3").setTabListener(new MyTabListener<FragmentPage3>(this,FragmentPage3.class)));
				
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu)
	{
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.login, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item)
	{
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings)
		{
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
}