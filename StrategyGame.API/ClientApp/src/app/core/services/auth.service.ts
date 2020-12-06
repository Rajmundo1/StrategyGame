import { Injectable } from '@angular/core';

import { Router} from '@angular/router';
import { tap } from 'rxjs/operators';
import { SignalRService } from './signal-r.service';
import { ApiAuthLogoutClient, ApiAuthRegisterClient, ApiAuthRenewClient, LoginClient, LoginDto, RegisterDto, TokenDto } from 'src/app/shared/clients';
import { Observable } from 'rxjs';



@Injectable()
export class AuthService {
    private readonly ACCESS_TOKEN = 'ACCESS_TOKEN';
    private readonly REFRESH_TOKEN = 'REFRESH_TOKEN';

    constructor(private logOutClient: ApiAuthLogoutClient,
                private registerClient: ApiAuthRegisterClient,
                private loginClient: LoginClient,
                private renewClient: ApiAuthRenewClient, 
                public router: Router,
                private signalRService: SignalRService) { }

    public getAccessToken(): string {
        return localStorage.getItem(this.ACCESS_TOKEN);
    }

    public isAuthenticated(): boolean {
        return !!this.getAccessToken();
    }

    login(name: string, pass: string): Observable<TokenDto> {
        const loginDto: LoginDto = new LoginDto({
            userName: name,
            password: pass
        });
        return this.loginClient.login(loginDto)
            .pipe(
                tap((token: TokenDto) => this.doLogin(token))
            );
    }

    logout() {
        return this.logOutClient.logout()
            .pipe(
                tap(_ => {
                    this.removeTokens();
                    this.signalRService.stopConnnection();
                })
            );
    }

    register(name: string, pass: string, county: string): Observable<TokenDto> {
        const registerDto: RegisterDto = new RegisterDto({
            userName: name,
            password: pass,
            countyName: county
        });
        return this.registerClient.register(registerDto)
            .pipe(
                tap((token: TokenDto) => this.doLogin(token))
            );
    }

    renew() {
        return this.renewClient.renewToken(this.getRefreshToken())
            .pipe(
                tap((token: TokenDto) => this.storeTokens(token))
            );
    }

    private doLogin(token: TokenDto) {
        this.storeTokens(token);
        this.signalRService.connectToHub();
    }

    private storeTokens(token: TokenDto) {
        if (token.accessToken != null) {
            localStorage.setItem(this.ACCESS_TOKEN, token.accessToken);
            localStorage.setItem(this.REFRESH_TOKEN, token.refreshToken);
        }
    }

    private removeTokens() {
        localStorage.removeItem(this.ACCESS_TOKEN);
        localStorage.removeItem(this.REFRESH_TOKEN);
    }

    private getRefreshToken() {
        return localStorage.getItem(this.REFRESH_TOKEN);
    }
}
