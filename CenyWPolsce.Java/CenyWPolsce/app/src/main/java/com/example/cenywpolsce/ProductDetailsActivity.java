package com.example.cenywpolsce;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

import com.example.cenywpolsce.abstracts.DatabaseActivity;
import com.example.cenywpolsce.adapters.PriceChangeAdapter;
import com.example.cenywpolsce.models.Product;
import com.github.mikephil.charting.charts.LineChart;
import com.github.mikephil.charting.components.XAxis;
import com.github.mikephil.charting.data.Entry;
import com.github.mikephil.charting.data.LineData;
import com.github.mikephil.charting.data.LineDataSet;
import com.github.mikephil.charting.formatter.IndexAxisValueFormatter;

import java.util.ArrayList;
import java.util.stream.Collectors;

public class ProductDetailsActivity extends DatabaseActivity {
    private TextView label;
    private ArrayList<Product> products;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_product_details);

        label = findViewById(R.id.choosenProductLabel);

        Intent received = getIntent();
        String productName = received.getStringExtra("product");
        label.setText(productName);
        products = db.get(productName);

        PriceChangeAdapter adapter = new PriceChangeAdapter(this, products, findViewById(R.id.price_history_list), findViewById(R.id.productsGroupingSwitch));

        //Initialize chart
        LineChart chart = findViewById(R.id.product_chart);
        ArrayList<Entry> entriesRise = new ArrayList<>();
        for (int i = 0; i < products.size(); i++) {
            entriesRise.add(new Entry(i, (float) products.get(i).Price));
        }
        LineDataSet set = new LineDataSet(entriesRise, "Cena produktu");
        set.setDrawValues(false);
        LineData riseData = new LineData(set);
        chart.setData(riseData);
        XAxis xrise = chart.getXAxis();
        xrise.setPosition(XAxis.XAxisPosition.BOTTOM);
        xrise.setDrawAxisLine(false);
        xrise.setGranularity(1f); // only intervals of 1 day
        xrise.setLabelCount(4);
        xrise.setValueFormatter(new IndexAxisValueFormatter(products.stream().map(e -> e.monthFormattedDate()).collect(Collectors.toList())));
        chart.invalidate();
    }
}