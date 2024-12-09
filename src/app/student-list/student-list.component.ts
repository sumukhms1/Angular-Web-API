import { Component, inject, OnInit } from '@angular/core';
import { StudentService,Student } from '../Service/student.service';
import { CommonModule } from '@angular/common';
import { StudentC } from '../Models/Class';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [FormsModule,CommonModule,],
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css'],
})
export class StudentListComponent implements OnInit {
  stdList: Student[] = [];
  studObj: StudentC = new StudentC


  constructor(private studentService: StudentService) { }
  http = inject(HttpClient)

  ngOnInit(): void {
    this.getAllStudents();
  }

  getAllStudents() {
    this.studentService.getStudents().subscribe((result:any)=>{
      this.stdList = result;
    })
  }
    
  onEdit(data:any){
    this.studObj =data;
  }

  onDelete(id : number){

    const isDelete = confirm("Are You Sure Want To Delete Student")

    if(isDelete){
      this.http.delete("https://localhost:7138/api/StudentAPI/DelteStudent/" +id).subscribe((res:any)=>{
        if(res.success){
          alert("Student Deleted Successfully")
          this.getAllStudents();
        }
        else{
          alert("Student isn't Deleted")
        }
      })
    }

  }

  onSave(){
    this.studentService.saveNewStudent(this.studObj).subscribe((res:any)=>{
      if(res.success){
        alert("Student Created Successfully")
        this.getAllStudents();
      }
      else{
        alert("Student Has't Created")
      }
      this.studObj = new StudentC(); 

    });
  }

  onUpdate(id:number){
    this.http.put("https://localhost:7138/api/StudentAPI/UpdateStudent/" +id,this.studObj).subscribe((res:any)=>{
      if(res.success){
        alert("Student Updated Successfully")
        this.getAllStudents();
      }
      else{
        alert("Student Not Updated")
      }
    })
  }
  
    
} 


  
  

