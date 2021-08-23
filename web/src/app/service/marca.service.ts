import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Marca } from '../models/marca';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class MarcaService {

  apiURL = 'https:localhost:5001';

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-type': 'application/json'
    })
  }

  getMarcas(): Observable<Marca> {
    return this.http.get<Marca>(this.apiURL + '/marcas')
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }

  createMarca(marca: any): Observable<Marca> {
    return this.http.post<Marca>(this.apiURL + '/marcas', JSON.stringify(marca), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  } 

  deleteMarca(id: number){
    return this.http.delete<Marca>(this.apiURL + '/marcas/destroy/' + id, this.httpOptions)
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
