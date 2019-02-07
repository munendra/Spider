import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[appConfirmDialog]'
})
export class ConfirmDialogDirective {

  constructor() { }


  @HostListener('click', ['$event'])
  public onClick(event: any): void {

  }

}
