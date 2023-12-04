package com.nbcc.cryptech

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.LayoutInflater
import android.view.Menu
import android.view.MenuInflater
import android.view.MenuItem
import androidx.databinding.DataBindingUtil
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.ViewModelProviders
import com.nbcc.cryptech.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    private lateinit var viewModel: ViewModel

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = DataBindingUtil.setContentView(this, R.layout.activity_main)
        viewModel = ViewModelProvider(this).get(ViewModel::class.java)

        binding.lifecycleOwner = this
        binding.vm = viewModel

        val adapter = EmployeeAdapter()
        binding.employeeList.adapter = adapter

        viewModel.departmentsChanged.observe(this, Observer {
           if (it) {
               this.invalidateOptionsMenu()
               viewModel.updateDepartmentsComplete()
           }
        })

        viewModel.employees.observe(this, Observer {
            it.let {
                adapter.submitList(it)
            }
        })

        viewModel.jobs.observe(this, Observer {
            it.let {
                adapter.jobs = it
            }
        })
    }

    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        for (department in viewModel.departments) {
            menu?.add(Menu.NONE, department.id, Menu.NONE, department.name)
        }

        menuInflater.inflate(R.menu.options_menu, menu)

        return super.onCreateOptionsMenu(menu)
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        viewModel.updateEmployees(item.itemId)
        return super.onOptionsItemSelected(item)
    }
}
