import React, {Component} from 'react';
import './refEx1.css'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as refEx1Actions from "../../store/refEx1/actions";
export default class refEx1 extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-ref-ex1">Hello! component refEx1</div>;
    }
  }
// export default connect(
//     ({ refEx1 }) => ({ ...refEx1 }),
//     dispatch => bindActionCreators({ ...refEx1Actions }, dispatch)
//   )( refEx1 );