import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Carro } from '../models/carro';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CarroService {

  apiURL = 'https:localhost:5001';

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-type': 'application/json',
      'mode': 'no-cors'
    })
  }

  getCarros(): Observable<Carro> {
    return this.http.get<Carro>(this.apiURL + '/carros')
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }

  createCarro(carro: any): Observable<Carro> {
    return this.http.post<Carro>(this.apiURL + '/carros', JSON.stringify(carro), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  } 

  deleteCarro(id: number){
    return this.http.delete<Carro>(this.apiURL + '/carros/destroy/' + id, this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }

  handleError(error: { error: { message: string; }; status: any; message: any; }) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) 
      errorMessage = error.error.message;
    else 
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;

    window.alert(errorMessage);
    return throwError(errorMessage);
 }
}
