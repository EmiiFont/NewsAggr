import { Component, OnInit } from '@angular/core';

interface Coin {
  id:                 string;
  name:               string;
  symbol:             string;
  rank:               string;
  price_usd:          number;
  price_btc:          string;
  percent_change_1h:  string;
  percent_change_24h: string;
  last_updated:       string;
}


@Component({
  selector: 'app-coin-price',
  templateUrl: './coin-price.component.html',
  styleUrls: ['./coin-price.component.css']
})

export class CoinPriceComponent implements OnInit {
  coins: Coin[] = [];
  availableCoins: any = {BTC: 0, ETH: 1, LTC: 2, XRP: 3, DASH: 4, NEO: 5, OMG: 6};
 
  constructor() { }

  ngOnInit() {
    fetch('https://api.coinmarketcap.com/v1/ticker/')
    .then(response => response.json() as Promise<Coin[]>)
    .then(data => {
      console.log(data);
      this.coins = data.filter(x => {
          if(this.availableCoins[x.symbol] != undefined) 
                 return x;
       });
    });
  }

  moneyFormat(price: string, sign = '$') {
    const pieces = parseFloat(price).toFixed(2).split('')
    let ii = pieces.length - 3
    while ((ii-=3) > 0) {
      pieces.splice(ii, 0, ',')
    }
    return sign + pieces.join('')
  }

}
