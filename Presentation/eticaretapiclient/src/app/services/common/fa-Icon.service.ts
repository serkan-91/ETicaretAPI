// icon.service.ts
import { Injectable } from '@angular/core';
import { faXmark, faPen } from '@fortawesome/free-solid-svg-icons';

@Injectable({
  providedIn: 'root'
})
export class FaIconService {
  faXmark = faXmark;
  faPen = faPen;
}
