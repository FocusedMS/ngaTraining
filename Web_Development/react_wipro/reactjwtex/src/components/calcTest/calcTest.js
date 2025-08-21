import React, {Component} from 'react';
import './calcTest.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as calcTestActions from "../../store/calcTest/actions";
export default class calcTest extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-calc-test">Hello! component calcTest</div>;
    }
  }
// export default connect(
//     ({ calcTest }) => ({ ...calcTest }),
//     dispatch => bindActionCreators({ ...calcTestActions }, dispatch)
//   )( calcTest );