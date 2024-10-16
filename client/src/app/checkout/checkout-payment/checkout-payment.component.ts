import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { Basket } from 'src/app/shared/models/basket';
import { Address } from 'src/app/shared/models/user';
import { NavigationExtras, Router } from '@angular/router';
import { loadStripe, Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement } from '@stripe/stripe-js';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {
  @Input() checkoutForm?: FormGroup;
  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;

  stripe: Stripe | null = null;
  cardNumber?: StripeCardNumberElement;
  cardExpiry?: StripeCardExpiryElement;
  cardCvc?: StripeCardCvcElement;
  cardErrors: any;

  constructor(private basketService: BasketService, private checkoutService: CheckoutService,
    private toastr: ToastrService,private router: Router) {}

  ngOnInit(): void {
    loadStripe('pk_test_51Q8lnxKfFIJF3xRMT46KRBBGJdRIFguu4ZjQeRRvvhPI3ORqWmyzaXDtQPFbsbB8gjWvWb4JqyWxpZQfH59emixv00OnPAJr09')
    .then(stripe => {
      this.stripe = stripe;
      const elements = stripe?.elements();

      if(elements) {
        this.cardNumber = elements.create('cardNumber');
        this.cardNumber.mount(this.cardNumberElement?.nativeElement);
        this.cardNumber.on('change', event => {
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardExpiry = elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
        this.cardExpiry.on('change', event => {
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardCvc = elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        this.cardCvc.on('change', event => {
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })
      }
    })
  }
  

    submitOrder() {
      const basket = this.basketService.getCurrentBasketValue();

      if(!basket) return;

      const orderToCreate = this.getOrderToCreate(basket)
      if (!orderToCreate) return;

      this.checkoutService.createOrder(orderToCreate).subscribe({
        next: order => {
          this.toastr.success('Order created succesfully');
          this.basketService.deleteLocalBasket();

          const navigationExtras: NavigationExtras = {state: order};
          this.router.navigate(['checkout/success'], navigationExtras);


          console.log(order);
        }
      })
    }
  
  private  getOrderToCreate(basket: Basket) {
    const deliverymethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
    const shipToAddress = this.checkoutForm?.get('addressForm')?.value as Address;
    if (!deliverymethodId || !shipToAddress) return;

    return {
      basketId: basket.id,
      deliveryMethodId: deliverymethodId,
      shipToAddress: shipToAddress
    }

  }

}
