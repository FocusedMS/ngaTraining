import React, {Component} from 'react';
import './buttonexampleTest.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as buttonexampleTestActions from "../../store/buttonexampleTest/actions";
export default class buttonexampleTest extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-buttonexample-test">Hello! component buttonexampleTest</div>;
    }
  }
// export default connect(
//     ({ buttonexampleTest }) => ({ ...buttonexampleTest }),
//     dispatch => bindActionCreators({ ...buttonexampleTestActions }, dispatch)
//   )( buttonexampleTest );