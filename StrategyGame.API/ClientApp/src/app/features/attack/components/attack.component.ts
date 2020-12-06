import { Component, Input, Output, ViewChild } from '@angular/core';
import { MatSlider } from '@angular/material/slider';
import { environment } from 'src/environments/environment';
import { UnitDto } from 'src/app/shared/clients';
import { IAttackViewModel } from '../models/attack.model';

@Component({
  selector: 'app-attack',
  templateUrl: './attack.component.html',
  styleUrls: ['./attack.component.scss']
})
export class AttackComponent {

  @Input() unit: IAttackViewModel;
  @Input() reset: boolean;

  @Output() @ViewChild(MatSlider) matslider: MatSlider;

  baseUrl = environment.apiUrl;

  constructor() { }

  addValue(): void{
    this.unit.sentCount = this.matslider.value;
    console.log(this.matslider.value);
  }

}
