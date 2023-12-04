package com.nbcc.cryptech

import android.text.util.Linkify
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView
import com.nbcc.cryptech.databinding.EmployeeBinding
import com.nbcc.cryptech.models.Employee
import com.nbcc.cryptech.models.Job

class EmployeeAdapter() : ListAdapter<Employee, EmployeeAdapter.EmployeeViewHolder>(DiffCallback) {
    var jobs = listOf<Job>()

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): EmployeeViewHolder {
        return EmployeeViewHolder.from(parent)
    }

    override fun onBindViewHolder(holder: EmployeeViewHolder, position: Int) {
        var employee: Employee = getItem(position)
        var job: Job = jobs.filter { j -> j.id == employee.jobId }.first()
        holder.bind(employee, job)
    }

    class EmployeeViewHolder(private var binding: EmployeeBinding) : RecyclerView.ViewHolder(binding.root) {
        fun bind(
            employee: Employee,
            job: Job
        ) {
            binding.employee = employee
            binding.job = job
            binding.clickListener = EmployeeListener {
                Linkify.addLinks(binding.email, Linkify.EMAIL_ADDRESSES)
                binding.email.linksClickable = true

                Linkify.addLinks(binding.phoneNumber, Linkify.PHONE_NUMBERS)
                binding.phoneNumber.linksClickable = true
            }
            binding.executePendingBindings()
        }

        companion object {
            fun from(parent: ViewGroup) : EmployeeViewHolder {
                val layoutInflater = LayoutInflater.from(parent.context)
                val binding = EmployeeBinding.inflate(layoutInflater, parent, false)
                return EmployeeViewHolder(binding)
            }
        }
    }

    companion object DiffCallback : DiffUtil.ItemCallback<Employee>() {
        override fun areItemsTheSame(oldItem: Employee, newItem: Employee): Boolean {
            return oldItem == newItem
        }

        override fun areContentsTheSame(oldItem: Employee, newItem: Employee): Boolean {
            return oldItem.id == newItem.id
        }
    }
}

class EmployeeListener(val clickListener: () -> Unit) {
    fun onClick() = clickListener()
}