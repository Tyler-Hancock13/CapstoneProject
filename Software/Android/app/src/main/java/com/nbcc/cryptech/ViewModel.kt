package com.nbcc.cryptech

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import com.nbcc.cryptech.models.Department
import com.nbcc.cryptech.models.Employee
import com.nbcc.cryptech.models.Job
import com.nbcc.cryptech.network.DepartmentService
import com.nbcc.cryptech.network.EmployeeService
import com.nbcc.cryptech.network.JobService
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.lang.Exception


class ViewModel : ViewModel() {
    private var job = kotlinx.coroutines.Job()
    private val coroutineScope = CoroutineScope(job + Dispatchers.Main)

    private val _departmentsChanged = MutableLiveData<Boolean>()
    val departmentsChanged : LiveData<Boolean>
        get() = _departmentsChanged

    private val _employees = MutableLiveData<List<Employee>>()
    val employees : LiveData<List<Employee>>
        get() = _employees

    private val _jobs = MutableLiveData<List<Job>>()
    val jobs : LiveData<List<Job>>
        get() = _jobs

    var departments = listOf(Department(0, "All"))

    init {
        coroutineScope.launch {
            getDepartments()
            getJobs()
            getEmployees(0)
        }
    }

    private suspend fun getDepartments() {
        try {
            var deferredDepartments = DepartmentService.retrofitService.getDepartments()
            var departmentList = deferredDepartments.await()
            departments = listOf(Department(0, "All")).plus(departmentList)
        } catch (e: Exception) {
            departments = listOf(Department(0, "All"))
        }

        _departmentsChanged.value = true
    }

    private suspend fun getEmployees(departmentId : Int) {
        try {
            if (departmentId == 0) {
                var deferredEmployees = EmployeeService.retrofitService.getEmployees()
                _employees.value = deferredEmployees.await().sortedBy { employee -> employee.lastName }
            } else {
                var deferredEmployees = EmployeeService.retrofitService.getByDepartment(departmentId)
                _employees.value = deferredEmployees.await().sortedBy { employee -> employee.lastName }
            }
        } catch (e: Exception) {
            _employees.value = listOf()
        }
    }

    fun updateEmployees(departmentId: Int) {
        coroutineScope.launch {
            getEmployees(departmentId)
        }
    }

    private suspend fun getJobs() {
        try {
            var deferredJobs = JobService.retrofitService.getAll()
            _jobs.value = deferredJobs.await()
        } catch (e: Exception) {
            _jobs.value = listOf()
        }
    }

    fun updateDepartmentsComplete() {
        _departmentsChanged.value = false;
    }
}