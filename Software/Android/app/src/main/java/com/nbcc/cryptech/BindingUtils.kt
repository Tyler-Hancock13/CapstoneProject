package com.nbcc.cryptech

import android.widget.TextView
import androidx.databinding.BindingAdapter
import com.nbcc.cryptech.models.Employee

@BindingAdapter("fullName")
fun TextView.setFullName(item: Employee) {
    text = if (item.middleInitial == null) {
        "${item.firstName} ${item.lastName}"
    } else {
        "${item.firstName} ${item.middleInitial} ${item.lastName}"
    }
}

@BindingAdapter("officeUnit")
fun TextView.setOfficeUnit(item: Employee) {
    text = if (item.officeUnit == 0) {
        ""
    } else {
        "Unit ${item.officeUnit}"
    }
}

@BindingAdapter("officeAddress")
fun TextView.setOfficeAddress(item: Employee) {
    text = "${item.officeAddress},"
}

@BindingAdapter("officeCity")
fun TextView.setOfficeCity(item: Employee) {
    text = if (item.officeUnit == 0) {
        "${item.officeCity}"
    } else {
        "${item.officeCity},"
    }
}