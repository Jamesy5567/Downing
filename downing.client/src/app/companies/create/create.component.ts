import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Company } from "../company";
import { CompaniesService } from "../companies.service";
import { Observable, catchError, map, of } from 'rxjs';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent implements OnInit {

  company: Company[] = [];
  createForm: FormGroup;
  errorMessage: string | null = null;

  constructor(
    public companiesService: CompaniesService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.createForm = this.formBuilder.group({
      companyName: ["", [
        Validators.required,
        Validators.maxLength(100),
        Validators.pattern('^[a-zA-Z0-9!@#$%^&*()_+\\-=\\[\\]{};\'"\\|,.<>\\/?]+$')]],
      code: ["", [
        Validators.required,
        Validators.maxLength(10),
        Validators.pattern('^[A-Z0-9]+$')],
        [this.uniqueValidator()]],
      sharePrice: ["", Validators.pattern('^-?\\d*(\\.\\d{0,5})?$')]
    });
  }

  ngOnInit(): void {
    this.companiesService.getCompanies().subscribe((data: Company[]) => {
      this.company = data;
    });
  }

  onSubmit() {
    
    const companyData: Company = this.createForm.value;        

    this.companiesService.createCompany(companyData).subscribe(res => {
      console.log('Record created successfully: ', res);
      this.createForm.reset();
      this.router.navigateByUrl('companies/index');
    },
    error => {
      this.errorMessage = error;
    });
  }

  uniqueValidator(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } | null> => {
      if (!control.value) {
        return of(null);
      } else {
        return this.companiesService.checkUnique(control.value).pipe(
          map(response => {
            return response.exists ? { unique: true } : null;
          }),
          catchError(() => of(null))
        );
      }
    };
  }
}
