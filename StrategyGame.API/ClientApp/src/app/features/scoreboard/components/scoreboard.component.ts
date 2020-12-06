import { Component, OnInit, Input} from '@angular/core';
import { UserDto } from 'src/app/shared/clients';

@Component({
  selector: 'app-scoreboard',
  templateUrl: './scoreboard.component.html',
  styleUrls: ['./scoreboard.component.scss']
})
export class ScoreboardComponent implements OnInit {

  @Input() user: UserDto;

  constructor() { }

  ngOnInit(): void {
  }

}
