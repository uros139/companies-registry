import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({ providedIn: 'root' })
export class CompanySearchService {
  private isinSubject = new BehaviorSubject<string | null>(null);
  isin$ = this.isinSubject.asObservable();

  setSearchIsin(isin: string) {
    this.isinSubject.next(isin);
  }
}