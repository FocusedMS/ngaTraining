import React, {Component} from 'react';
import './memo2.css'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as memo2Actions from "../../store/memo2/actions";
export default class memo2 extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-memo2">Hello! component memo2</div>;
    }
  }
// export default connect(
//     ({ memo2 }) => ({ ...memo2 }),
//     dispatch => bindActionCreators({ ...memo2Actions }, dispatch)
//   )( memo2 );