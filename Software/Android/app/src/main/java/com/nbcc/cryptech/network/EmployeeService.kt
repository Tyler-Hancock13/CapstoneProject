package com.nbcc.cryptech.network

import com.jakewharton.retrofit2.adapter.kotlin.coroutines.CoroutineCallAdapterFactory
import com.nbcc.cryptech.models.Employee
import com.squareup.moshi.Moshi
import com.squareup.moshi.kotlin.reflect.KotlinJsonAdapterFactory
import kotlinx.coroutines.Deferred
import retrofit2.Retrofit
import retrofit2.converter.moshi.MoshiConverterFactory
import retrofit2.http.GET
import retrofit2.http.Path

private const val BASE_URL = "http://10.0.2.2:44349/api/"

private val moshi = Moshi.Builder()
    .add(KotlinJsonAdapterFactory())
    .build()

private val retrofit = Retrofit.Builder()
    .addConverterFactory(MoshiConverterFactory.create(moshi))
    .addCallAdapterFactory(CoroutineCallAdapterFactory())
    .baseUrl(BASE_URL)
    .build()

interface IEmployeeService {
    @GET("employees")
    fun getEmployees(): Deferred<List<Employee>>

    @GET("employees/department/{id}")
    fun getByDepartment(@Path("id") id: Int) : Deferred<List<Employee>>
}

object EmployeeService {
    val retrofitService: IEmployeeService by lazy {
        retrofit.create(IEmployeeService::class.java)
    }
}