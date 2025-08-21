import React, {Component} from 'react';
import './testcomponentTest.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as testcomponentTestActions from "../../store/testcomponentTest/actions";
export default class testcomponentTest extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-testcomponent-test">Hello! component testcomponentTest</div>;
    }
  }
// export default connect(
//     ({ testcomponentTest }) => ({ ...testcomponentTest }),
//     dispatch => bindActionCreators({ ...testcomponentTestActions }, dispatch)
//   )( testcomponentTest );