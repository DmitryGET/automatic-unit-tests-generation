import { Directive, ElementRef, HostListener, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appHighlightAndShowButton]',
  standalone: true,
})
export class HighlightAndShowButtonDirective {
  private button!: HTMLElement;

  constructor(private el: ElementRef, private renderer: Renderer2) {
    this.createButton();
  }

  private createButton() {
    this.button = this.renderer.createElement('button');
    this.renderer.setProperty(this.button, 'innerText', 'Взаимодействие');
    this.renderer.setStyle(this.button, 'display', 'none');
    this.renderer.setStyle(this.button, 'position', 'absolute');
    this.renderer.setStyle(this.button, 'z-index', '1000');
    this.renderer.appendChild(document.body, this.button);
  }

  @HostListener('mouseup', ['$event'])
  onMouseUp(event: MouseEvent) {
    const selection = window.getSelection();
    if (selection && selection.toString().trim()) {
      const range = selection.getRangeAt(0);
      const rect = range.getBoundingClientRect();

      this.renderer.setStyle(
        this.button,
        'top',
        `${rect.bottom + window.scrollY}px`
      );
      this.renderer.setStyle(
        this.button,
        'left',
        `${rect.left + window.scrollX}px`
      );
      this.renderer.setStyle(this.button, 'display', 'block');
    } else {
      this.hideButton();
    }
  }

  @HostListener('document:click')
  onClick() {
    this.hideButton();
  }

  private hideButton() {
    this.renderer.setStyle(this.button, 'display', 'none');
  }
}
