import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection} from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { RefreshDataService } from './refresh-data.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService{
  private connection: HubConnection;

  constructor(private refreshService: RefreshDataService) { 
   this.connection = new HubConnectionBuilder()
    .withUrl(`${environment.apiUrl}/api/newround`)
    .build();
  }

  connectToHub() {
    this.start();
  }

  stopConnnection() {
    this.connection.stop();
    console.log('Disconnected from SignalR hub');
  }

  private async start() {
    await this.connection.start()
      .then(_ => {
        console.log('Connected to SignalR hub');
        this.connection.on('NewRound', _ => this.refreshService.refresh(true));
      })
      .catch(error => {
        console.log(error);
        setTimeout(this.start, 5000);
      });
  }
}
