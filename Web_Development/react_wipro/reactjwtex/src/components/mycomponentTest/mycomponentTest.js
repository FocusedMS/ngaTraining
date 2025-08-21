import React, {Component} from 'react';
import './mycomponentTest.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as mycomponentTestActions from "../../store/mycomponentTest/actions";
export default class mycomponentTest extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-mycomponent-test">Hello! component mycomponentTest</div>;
    }
  }
// export default connect(
//     ({ mycomponentTest }) => ({ ...mycomponentTest }),
//     dispatch => bindActionCreators({ ...mycomponentTestActions }, dispatch)
//   )( mycomponentTest );