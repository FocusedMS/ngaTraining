import React, {Component} from 'react';
import './calc.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as calcActions from "../../store/calc/actions";
export default class calc extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-calc">Hello! component calc</div>;
    }
  }
// export default connect(
//     ({ calc }) => ({ ...calc }),
//     dispatch => bindActionCreators({ ...calcActions }, dispatch)
//   )( calc );