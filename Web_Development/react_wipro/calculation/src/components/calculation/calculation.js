import React, {Component} from 'react';
import './calculation.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as calculationActions from "../../store/calculation/actions";
export default class calculation extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-calculation">Hello! component calculation</div>;
    }
  }
// export default connect(
//     ({ calculation }) => ({ ...calculation }),
//     dispatch => bindActionCreators({ ...calculationActions }, dispatch)
//   )( calculation );