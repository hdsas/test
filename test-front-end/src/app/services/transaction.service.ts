import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private http: HttpClient) { }


  upload(formData: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}transection/upload`, formData);
  }

  search(form:any):Observable<any>{
     return this.http.post(`${environment.apiUrl}transection/search`, form);
  }

}
