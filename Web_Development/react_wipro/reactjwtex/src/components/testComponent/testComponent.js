import React, {Component} from 'react';
import './testComponent.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as testComponentActions from "../../store/testComponent/actions";
export default class testComponent extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-test-component">Hello! component testComponent</div>;
    }
  }
// export default connect(
//     ({ testComponent }) => ({ ...testComponent }),
//     dispatch => bindActionCreators({ ...testComponentActions }, dispatch)
//   )( testComponent );