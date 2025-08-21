import React, {Component} from 'react';
import './nameForm.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as nameFormActions from "../../store/nameForm/actions";
export default class nameForm extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-name-form">Hello! component nameForm</div>;
    }
  }
// export default connect(
//     ({ nameForm }) => ({ ...nameForm }),
//     dispatch => bindActionCreators({ ...nameFormActions }, dispatch)
//   )( nameForm );