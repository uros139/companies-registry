import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { CompanySearchService } from '../../services/company-search.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit, OnDestroy {
  searchControl = new FormControl('');
  private destroy$ = new Subject<void>();

  constructor(
    private searchService: CompanySearchService,
    private router: Router) { }

  ngOnInit() {
    this.searchControl.valueChanges.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe(value => {
      this.searchService.setSearchIsin((value ?? '').trim());
    });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onLogout() {
    localStorage.removeItem('jwt_token');
    this.router.navigate(['/login']);
  }
}