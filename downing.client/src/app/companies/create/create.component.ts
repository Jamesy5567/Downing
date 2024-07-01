import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Company } from "../company";
import { CompaniesService } from "../companies.service";

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent implements OnInit {

  company: Company[] = [];
  createForm;

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
      companyCode: ["", [
        Validators.required,
        Validators.maxLength(10),
        Validators.pattern('^[A-Z0-9]+$')]],
      sharePrice: ["", Validators.pattern('^-?\\d*(\\.\\d{0,5})?$')]
    });
  }

  ngOnInit(): void {
    this.companiesService.getCompanies().subscribe((data: Company[]) => {
      this.company = data;
    });
  }

  onSubmit(formData: any) {
    console.log(formData.value);

    this.companiesService.createCompany(formData.value).subscribe(res => {
      this.router.navigateByUrl('companies/index');
    });
  }

}
