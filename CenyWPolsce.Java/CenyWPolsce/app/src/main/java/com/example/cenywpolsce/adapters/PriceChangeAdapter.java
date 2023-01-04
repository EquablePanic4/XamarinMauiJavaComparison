package com.example.cenywpolsce.adapters;

import android.content.Context;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.CompoundButton;
import android.widget.Switch;
import android.widget.TextView;

import androidx.annotation.Nullable;
import androidx.constraintlayout.widget.ConstraintLayout;

import com.example.cenywpolsce.R;
import com.example.cenywpolsce.models.Product;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.zip.Inflater;

public class PriceChangeAdapter {
    LayoutInflater inflater;
    private ViewGroup parent;
    ArrayList<ProductPriceBackend> elements;

    public PriceChangeAdapter(Context context, ArrayList<Product> productsList, ViewGroup parentElement, Switch groupingSwitch) {
        parent = parentElement;
        inflater = LayoutInflater.from(context);

        elements = new ArrayList<>();
        for (int i = 0; i < productsList.size(); i++) {
            Product prev = null;
            if (i > 0)
                prev = productsList.get(i - 1);

            elements.add(new ProductPriceBackend(inflater, i, productsList.get(i), prev));
        }

        //Switch initializing
        groupingSwitch.setOnCheckedChangeListener((buttonView, isChecked) -> {
            Initialize(isChecked);
        });

        //Initializing
        Initialize(groupingSwitch.isChecked());
    }

    public void Initialize(boolean grouped) {
        parent.removeAllViews();
        int index = 0;

        ProductPriceBackend last = elements.get(0);
        if (grouped) {
            for (int i = 0; i < elements.size(); i++) {
                if (last.price != elements.get(i).price) {
                    if (i > 0) {
                        last.addDataDuration(elements.get(i - 1).dataFormatted);
                    }

                    last.changeColor(index++);
                    parent.addView(last.getView());
                    last = elements.get(i);
                }
            }
        }

        else {
            for (ProductPriceBackend b : elements) {
                b.changeColor(index++);
                parent.addView(b.getView());
            }
        }
    }

    class ProductPriceBackend {
        private DecimalFormat doubleFormatter = new DecimalFormat("#.##");
        private View view;
        public double price;
        public String dataFormatted;
        private TextView date;
        ConstraintLayout background;

        public ProductPriceBackend(LayoutInflater inflater, int index, Product product, Product previous) {
            view = inflater.inflate(R.layout.product_price_change_fragment, parent, false);
            price = product.Price;
            dataFormatted = product.monthFormattedDate();

            background = view.findViewById(R.id.product_list_history_bckg);
            date = view.findViewById(R.id.price_history_date);
            TextView priceTextView = view.findViewById(R.id.price_history_price);
            TextView change = view.findViewById(R.id.price_history_change);

            date.setText(product.monthFormattedDate());
            priceTextView.setText(product.Price + " zł");

            String changeText = "";
            if (previous != null) {
                double changeValue = (((product.Price - previous.Price) / previous.Price) * 100);

                if (changeValue > 0) {
                    changeText = "+" + doubleFormatter.format(changeValue) + "%";
                    change.setTextColor(Color.GREEN);
                }

                else {
                    if (changeValue < 0) {
                        changeText = doubleFormatter.format(changeValue) + "%";
                        change.setTextColor(Color.RED);
                    }

                    else
                        changeText = doubleFormatter.format(changeValue) + "%";
                }
            }

            change.setText(changeText);
        }

        public void addDataDuration(String formattedDate) {
            if (!date.getText().equals(formattedDate))
                date.setText(date.getText() + " - " + formattedDate);
        }

        public View getView() {
            return view;
        }

        public void changeColor(int position) {
            if (position % 2 != 0) {
                background.setBackgroundColor(Color.WHITE);
            }

            else
                background.setBackgroundColor(Color.parseColor("#EDEDED"));
        }
    }
}
