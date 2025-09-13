import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { ZXingScannerModule } from '@zxing/ngx-scanner';
import { BarcodeFormat } from '@zxing/library';
import { TicketCheckResponseDto, TicketClient } from '../../services/moziHttpClient';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-ticketcheck',
  imports: [
    LocalSharedModule,
    ZXingScannerModule
  ],
  templateUrl: './admin-ticketcheck.html',
  styleUrl: './admin-ticketcheck.css'
})
export class AdminTicketcheck implements OnInit {

  allowedFormats = [BarcodeFormat.QR_CODE, BarcodeFormat.DATA_MATRIX, BarcodeFormat.AZTEC];
  scannedResult: string | null = null;
  hasDevices = false;
  availableDevices: MediaDeviceInfo[] = [];
  selectedDevice: MediaDeviceInfo | undefined;  // Fix: Use `undefined` instead of `null`

  showResultDialog: boolean = false;
  checkResultDto: TicketCheckResponseDto = {
    message: "",
    movieDate: "",
    movieName: "",
    valid: false
  } as TicketCheckResponseDto

  constructor(
    private ticketClient: TicketClient,
    private userAuth: AuthService,
    private router: Router
  ){}

  ngOnInit(): void {
    if(!this.userAuth.isAdminOrEmployee) {
      this.router.navigate(['kezdolap']).then(() => {
        window.location.reload();
      });
    }
  }
  
  onCodeResult(result: string) {
    this.scannedResult = result;
    console.log(this.scannedResult)

    this.ticketClient.checkTicketValidity(this.scannedResult).subscribe(x=> {

      // let validity = x.valid ? "ÉRVÉNYES" : "ÉRVÉNYTELEN!!!"
      // alert(
      //   "Film: " + x.movieName + "\n" +
      //   "Dátum: " + x.movieDate + "\n" +
      //   "Üzenet: " + x.message + "\n" +
      //   validity
      // )

      this.checkResultDto = x;
      this.showResultDialog = true;
    })
  }

  onDeviceSelectChange(event: Event) {
    const target = event.target as HTMLSelectElement; // Fix: Properly typecast EventTarget
    this.selectedDevice = this.availableDevices.find(device => device.deviceId === target.value);
  }

  onHasDevices(hasDevices: boolean) {
    this.hasDevices = hasDevices;
  }

  onDevicesFound(devices: MediaDeviceInfo[]) {
    this.availableDevices = devices;
    if (devices.length > 0) {
      this.selectedDevice = devices[0]; // Fix: Ensure a device is selected initially
    }
  }

  onError(error: any) {
    console.error('Barcode scanning error:', error);
  }
}
