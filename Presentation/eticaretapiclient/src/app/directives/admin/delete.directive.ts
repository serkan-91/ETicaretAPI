import {   ApplicationRef,      Directive, ElementRef, Injectable, Injector, Input, Renderer2, ViewChild, ViewContainerRef  } from '@angular/core';
import { HttpClientService } from '../../services/common/http-client.service';
import { FaIconService } from '../../services/common/fa-Icon.service'; 
import { MatButton,   MatButtonModule, MatFabButton } from '@angular/material/button';

@Directive({
  selector: '[appDelete]'
})
 
export class DeleteDirective   {
  @Input() color: string = 'primary'; // Varsayılan renk
  constructor(
    private vcr: ViewContainerRef,
    private elr: ElementRef,
    private renderer: Renderer2, 
    private httpClientService: HttpClientService,
    private faIconService: FaIconService, 
    private injector: Injector,
    private appRef: ApplicationRef, 
  ) { }
  @ViewChild('matButton', { static: true, read: ElementRef }) matdButton: ElementRef;


  ngOnInit() {
     
    //// MatButton bileşeni oluşturun

    const component = this.vcr.createComponent(MatButton["button[mat-raised-button"])
   
  

    //const a = matbutton.injector.get.prototype

    //var deletebtn = matbutton.location.nativeElement


    //// MatButton bileşenini DOM'a ekleyin
    //this.renderer.appendChild(this.el.nativeElement, matbutton.location.nativeElement);


    //// Diğer özellikleri ayarlayabilirsiniz
    //const matButtonInstance = matbutton.instance;

    //matButtonInstance.color = 'warn';
    //matButtonInstance._elementRef.nativeElement
    //// Düğme metnini ayarlayın
    //matButtonInstance._elementRef.nativeElement.textContent = 'tesxt';

    //const factory = this.componentFactoryResolver.resolveComponentFactory(MatButton);
    //const componentRef = this.vcr.createComponent(factory);

    //// Add 'mat-raised-button' attribute
    //const el: HTMLElement = componentRef.location.nativeElement;
    //this.renderer.setAttribute(el, 'mat-raised-button', '');

    //// Set other properties
    //componentRef.instance.color = 'accent';
  }
}
