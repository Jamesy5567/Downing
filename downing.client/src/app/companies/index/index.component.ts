import { Component, OnInit } from '@angular/core';
import { Company } from '../company';
import { CompaniesService } from '../companies.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrl: './index.component.css'
})
export class IndexComponent implements OnInit {
  companies: Company[] = [];

  constructor(public companiesService: CompaniesService) { }

  /** Function to call the Companies-Service to fetch companies. */
  ngOnInit(): void {
    this.companiesService.getCompanies().subscribe((data: Company[]) => {
      this.companies = data;
    });
  }
}
