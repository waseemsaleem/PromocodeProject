import React, { Component } from 'react';
import { Button, Form, FormControl, FormGroup } from 'react-bootstrap';

class PromoFilter extends Component {
    state = {
        title: ""
    }

    handleTitleChange = ({ currentTarget: input }) => {
        this.setState({ title: input.value });
        this.props.doFilter(input.value);
    };

    handleReset = () => {
        this.setState({ title: "" });
        this.props.doFilter("");
    }
    render() {
        const { title } = this.state;
        return ( 
            <div>
                <div className="input-group">
                    <Form inline>
                        <p>Filter</p>
                        <FormGroup controlId="formInlineEmail">
                            <FormControl type="text" value={title} onChange={this.handleTitleChange} />
                        </FormGroup>{" "}
                        <Button type="reset" bsStyle="default" className="reset" onClick= {this.handleReset}>
                        Reset
                        </Button>
                    </Form>
                </div>
            </div>
         );
    }
}
 
export default PromoFilter;