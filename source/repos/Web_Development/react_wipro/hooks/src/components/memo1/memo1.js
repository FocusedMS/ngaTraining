import React, {Component} from 'react';
import './memo1.css'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as memo1Actions from "../../store/memo1/actions";
export default class memo1 extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-memo1">Hello! component memo1</div>;
    }
  }
// export default connect(
//     ({ memo1 }) => ({ ...memo1 }),
//     dispatch => bindActionCreators({ ...memo1Actions }, dispatch)
//   )( memo1 );