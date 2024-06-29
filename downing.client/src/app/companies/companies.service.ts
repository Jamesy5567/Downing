import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Company } from "./company";

@Injectable({
  providedIn: 'root'
})
export class CompaniesService {

  private apiUrl = "https://localhost:7094/api";
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient) { }

  getCompanies(): Observable<Company[]> {
    return this.httpClient.get<Company[]>(this.apiUrl + '/companies')
      .pipe(
        catchError(this.errorHandler)
      );
  }

  createCompany(company: Company): Observable<Company> {
    return this.httpClient.post<Company>(this.apiUrl + '/companies', JSON.stringify(company), this.httpOptions)
      .pipe(
        catchError(this.errorHandler)
      )
  }

  errorHandler(error: { error: { message: string; }; }) {
    let errorMessage = '';

    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    }
    else {
      errorMessage = 'Error Code: ${error.status}\nMessage: ${error.message}';
    }
    return throwError(errorMessage);
  }
}
