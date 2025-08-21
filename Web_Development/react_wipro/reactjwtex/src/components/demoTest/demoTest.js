import React, {Component} from 'react';
import './demoTest.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as demoTestActions from "../../store/demoTest/actions";
export default class demoTest extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-demo-test">Hello! component demoTest</div>;
    }
  }
// export default connect(
//     ({ demoTest }) => ({ ...demoTest }),
//     dispatch => bindActionCreators({ ...demoTestActions }, dispatch)
//   )( demoTest );