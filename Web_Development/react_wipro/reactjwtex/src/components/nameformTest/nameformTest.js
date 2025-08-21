import React, {Component} from 'react';
import './nameformTest.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as nameformTestActions from "../../store/nameformTest/actions";
export default class nameformTest extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-nameform-test">Hello! component nameformTest</div>;
    }
  }
// export default connect(
//     ({ nameformTest }) => ({ ...nameformTest }),
//     dispatch => bindActionCreators({ ...nameformTestActions }, dispatch)
//   )( nameformTest );