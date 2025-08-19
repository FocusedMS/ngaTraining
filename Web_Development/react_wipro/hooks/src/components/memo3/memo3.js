import React, {Component} from 'react';
import './memo3.css'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as memo3Actions from "../../store/memo3/actions";
export default class memo3 extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-memo3">Hello! component memo3</div>;
    }
  }
// export default connect(
//     ({ memo3 }) => ({ ...memo3 }),
//     dispatch => bindActionCreators({ ...memo3Actions }, dispatch)
//   )( memo3 );