import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Categoria } from '../models/categoria';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

  apiURL = 'https:localhost:5001';

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-type': 'application/json'
    })
  }

  getCategorias(): Observable<Categoria> {
    return this.http.get<Categoria>(this.apiURL + '/categorias')
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }

  createCategoria(categoria: any): Observable<Categoria> {
    return this.http.post<Categoria>(this.apiURL + '/categorias', JSON.stringify(categoria), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  } 

  deleteCategoria(id: number){
    return this.http.delete<Categoria>(this.apiURL + '/categorias/destroy/' + id, this.httpOptions)
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
