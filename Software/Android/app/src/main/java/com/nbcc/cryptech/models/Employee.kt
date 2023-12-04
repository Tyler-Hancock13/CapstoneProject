package com.nbcc.cryptech.models

import com.squareup.moshi.Json

data class Employee (
    val id: String,
    val firstName: String,
    val lastName: String,
    val middleInitial: String?,
    val workPhone: String?,
    val cellPhone: String?,

    @Json(name = "emailAddress")
    val email : String,

    val officeAddress: String,
    val officeCity: String,
    val officeUnit: Int,

    @Json(name = "jobID")
    val jobId: Int
)