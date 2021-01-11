import React from "react";
import { Component } from "react";
import "../../css/home.css";
import { Form, FormGroup, Col, FormControl, Button, Row, Glyphicon, Alert, } from "react-bootstrap";
import promoService from '../../services/promocodeService';
import PromoFilter from "./FilterPromo";
import { CopyToClipboard } from 'react-copy-to-clipboard';
import toaster from '../../utils/Toaster';

class Promos extends Component {
  state = { 
    promos: [],
    message: "",
    error: false,
    searchTitle: "",
  }
  
  async componentDidMount() {
    await this.populatePromos();
  }

  populatePromos = async (title) => {
    const res = await promoService.getAllPromoService(title);
    if (res && res.success) {
      this.setState({ promos: res.data, searchTitle:title });
    } else {
      const message = res && res.message ? res.message : "Failed to load Promo Services data";
      this.setState({ error: true, message: message, searchTitle: title });
    }
  }

  toggleActivation = async (promo) => {
      await promoService.togglePromoActivation({ toggleActivation: !promo.activated, promoCodeId: promo.id });
      await this.populatePromos(this.state.searchTitle);
  }
    
  filterPromo = async (title) => {
    await this.populatePromos(title);
  }
    
  handleCopyClipboard = (promo) => {
    toaster.success(`${promo.promoCode} copied`, toaster.TOP_RIGHT, 5000);
  }
  
  renderPromo = (promo) => {
    return (
      <div className="card" key={promo.id}>
        <Row>
          <Col sm={6} className="card-info">
            <h1 className="card-title">{promo.title}</h1>
            <p className="card-desc">{promo.description}</p>
          </Col>
          <Col sm={3} className="promo-code">
            <Form inline>
              <p>Promocode</p>
              <FormGroup controlId="formInlineEmail">
                <FormControl type="text" readOnly value={promo.promoCode} />
                <CopyToClipboard text={promo.promoCode} onCopy={() => this.handleCopyClipboard(promo)}>
                    <span><Glyphicon glyph='copy' className="copy-clipboard" /></span>
                </CopyToClipboard>
              </FormGroup>{" "}
            </Form>
          </Col>
          <Col sm={3}>
            <Button type="submit" className="activate-btn" onClick={()=> this.toggleActivation(promo)}>
                {promo.activated ? "Deactivate Bonus" : "Activate bonus"}
            </Button>
          </Col>
        </Row>
      </div>
    );
  }

  render() { 
    const { error, message, promos } = this.state;
    return ( 
        <div>
        {error &&
          <Alert color="danger">
            {message}
          </Alert>
        }
        {!error &&
          <div>
            <h1>Services</h1>
            <PromoFilter doFilter = {this.filterPromo} />
            <div className="card-group">
              {promos.map( promo => this.renderPromo(promo))}
            </div>
          </div>
        }
        </div>
     );
  }
}

export default Promos;
