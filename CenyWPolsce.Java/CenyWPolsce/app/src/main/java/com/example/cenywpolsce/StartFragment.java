package com.example.cenywpolsce;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.fragment.app.Fragment;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.example.cenywpolsce.abstracts.DatabaseFragment;
import com.example.cenywpolsce.data.DbContext;
import com.example.cenywpolsce.models.Product;
import com.github.mikephil.charting.charts.LineChart;
import com.github.mikephil.charting.components.XAxis;
import com.github.mikephil.charting.data.Entry;
import com.github.mikephil.charting.data.LineData;
import com.github.mikephil.charting.data.LineDataSet;
import com.github.mikephil.charting.formatter.IndexAxisValueFormatter;
import com.github.mikephil.charting.formatter.ValueFormatter;

import java.util.ArrayList;
import java.util.stream.Collectors;

public class StartFragment extends DatabaseFragment {

    private TextView productCountLabel;
    private TextView recordsCountLabel;
    private LineChart growChart;
    private LineChart fallChart;

    public StartFragment(Context context) {
        super(context);
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_start, container, false);
    }

    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        productCountLabel = view.findViewById(R.id.productNumberLabel);
        recordsCountLabel = view.findViewById(R.id.allRecordsLabel);
        growChart = view.findViewById(R.id.chartGrow);
        fallChart = view.findViewById(R.id.chartFall);

        productCountLabel.setText(db.productsCount() + "");
        recordsCountLabel.setText(db.count() + "");

        //Initialize charts...
        {
            ArrayList<Product> mostRise = db.mostPriceRise();
            ArrayList<Entry> entriesRise = new ArrayList<>();
            for (int i = 0; i < mostRise.size(); i++) {
                entriesRise.add(new Entry(i, (float) mostRise.get(i).Price));
            }
            LineDataSet riseSet = new LineDataSet(entriesRise, "Cena produktu");
            riseSet.setDrawValues(false);
            LineData riseData = new LineData(riseSet);
            growChart.setData(riseData);
            XAxis xrise = growChart.getXAxis();
            xrise.setPosition(XAxis.XAxisPosition.BOTTOM);
            xrise.setDrawAxisLine(false);
            xrise.setGranularity(1f); // only intervals of 1 day
            xrise.setLabelCount(4);
            xrise.setValueFormatter(new IndexAxisValueFormatter(mostRise.stream().map(e -> e.monthFormattedDate()).collect(Collectors.toList())));
            growChart.invalidate();
            TextView label = view.findViewById(R.id.mostRiseLabel);
            label.setText(mostRise.get(0).Name);

            //Event listeners
            view.findViewById(R.id.mostRiseBtn).setOnClickListener(v -> {
                Intent intent = new Intent(ctx, ProductDetailsActivity.class);
                intent.putExtra("product", mostRise.get(0).Name);

                startActivity(intent);
            });
        }

        {
            ArrayList<Product> mostFall = db.mostPriceFall();
            ArrayList<Entry> entriesFall = new ArrayList<>();
            for (int i = 0; i < mostFall.size(); i++) {
                entriesFall.add(new Entry(i, (float) mostFall.get(i).Price));
            }
            LineDataSet riseSet = new LineDataSet(entriesFall, "Cena produktu");
            riseSet.setDrawValues(false);
            LineData riseData = new LineData(riseSet);
            fallChart.setData(riseData);
            XAxis xrise = fallChart.getXAxis();
            xrise.setPosition(XAxis.XAxisPosition.BOTTOM);
            xrise.setDrawAxisLine(false);
            xrise.setGranularity(1f); // only intervals of 1 day
            xrise.setLabelCount(4);
            xrise.setValueFormatter(new IndexAxisValueFormatter(mostFall.stream().map(e -> e.monthFormattedDate()).collect(Collectors.toList())));
            fallChart.invalidate();
            TextView label = view.findViewById(R.id.mostFallLabel);
            label.setText(mostFall.get(0).Name);

            //Event listeners
            view.findViewById(R.id.mostFallBtn).setOnClickListener(v -> {
                Intent intent = new Intent(ctx, ProductDetailsActivity.class);
                intent.putExtra("product", mostFall.get(0).Name);

                startActivity(intent);
            });
        }
    }
}