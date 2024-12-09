import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StudentC } from '../Models/Class';
import { Observable } from 'rxjs';

export interface Student {
  id: number;
  studentName: string;
  studentGender:string;
  age: number;
  grade:number;
  fatherName: string;
}

@Injectable({
  providedIn: 'root'
})

export class StudentService {
   apiUrl:string = 'https://localhost:7138/api/StudentAPI/';  


  constructor(private http: HttpClient) { }

  

  getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.apiUrl + "GetStudents");
  }
  

  saveNewStudent(obj:any){
    return this.http.post(this.apiUrl + "NewStudent",obj)
  }

  updateStudent(obj:any){
    return this.http.put(this.apiUrl + "UpdateStudent"  ,obj)
  }

  

  






}

  