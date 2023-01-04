package com.example.cenywpolsce.models;

import android.database.Cursor;
import android.util.Log;

import java.security.PublicKey;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

public class Product implements Comparable<Product> {

    static SimpleDateFormat csharpDateFormatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
    static SimpleDateFormat monthDateFormatter = new SimpleDateFormat("MM-yyyy");

    public String Id;
    public java.util.Date Date;
    public String Name;
    public double Price;

    public Product() {

    }

    public Product(String id, Date date, String name, double price) {
        Id = id;
        Date = date;
        Name = name;
        Price = price;
    }

    public Product(String id, String date, String name, double price) {
        Id = id;

        try {
            Date = csharpDateFormatter.parse(date);
        }

        catch (Exception ex) {
            Log.v("Parse error", "Couldn't parse date from db");
        }
        Name = name;
        Price = price;
    }

    public Product(Cursor cursor) {
        this(
                cursor.getString(cursor.getColumnIndexOrThrow("Id")),
                cursor.getString(cursor.getColumnIndexOrThrow("Data")),
                cursor.getString(cursor.getColumnIndexOrThrow("Nazwa")),
                cursor.getDouble(cursor.getColumnIndexOrThrow("Cena"))
        );
    }

    @Override
    public boolean equals(Object o) {
        if (o instanceof Product) {
            Product p = (Product)o;
            return p.Id.equals(Id);
        }

        return false;
    }

    @Override
    public int compareTo(Product o) {
        return 0;
    }

    @Override
    public String toString() {
        return Name;
    }

    public String monthFormattedDate() {
        return monthDateFormatter.format(Date);
    }
}
